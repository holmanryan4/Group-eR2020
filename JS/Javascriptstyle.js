function polarToCartesian(centerX, centerY, radius, angleInDegrees) {
  var angleInRadians = (angleInDegrees-90) * Math.PI / 180.0;

  return {
    x: centerX + (radius * Math.cos(angleInRadians)),
    y: centerY + (radius * Math.sin(angleInRadians))
  };
}

function describeArc(x, y, radius, startAngle, endAngle){

    var start = polarToCartesian(x, y, radius, endAngle);
    var end = polarToCartesian(x, y, radius, startAngle);

    var largeArcFlag = endAngle - startAngle <= 180 ? "0" : "1";

    var d = [
        "M", start.x, start.y, 
        "A", radius, radius, 0, largeArcFlag, 0, end.x, end.y
    ].join(" ");

    return d;       
}

function debounce(func, wait, immediate) {
	var timeout
	return function() {
		var context = this, args = arguments
		var later = function() {
			timeout = null
			if (!immediate) func.apply(context, args)
		};
		var callNow = immediate && !timeout
		clearTimeout(timeout)
		timeout = setTimeout(later, wait)
		if (callNow) func.apply(context, args)
	}
}

new Vue({
	el: root,
	data: {
		viewBoxWidth: 0,
		viewBoxHeight: 0,
		r1: 200,
		r2: 240,
		str1: 28,
		arc1: {
			startAngle: 10,
			endAngle: 240
		},
		arc2: {
			startAngle: 190,
			endAngle: 420
		}
	},
	computed: {
		pathArc1() {
			return describeArc(this.viewBoxWidth/2, this.viewBoxHeight/2, this.r1, this.arc1.startAngle, this.arc1.endAngle)
		},
		pathArc2() {
			return describeArc(this.viewBoxWidth/2, this.viewBoxHeight/2, this.r2, this.arc2.startAngle, this.arc2.endAngle)
		},
		outerRad() {
			return this.viewBoxWidth / 1.4
		}
	},
	
	mounted() {
		this.$nextTick(() => {
			// window.addEventListener('resize', this.setViewboxDebounced())
			this.setViewbox()
			this.animateAll()
		})
	},
	
	methods: {
		setViewbox() {
			this.viewBoxWidth = Math.floor( document.documentElement.clientWidth)
			this.viewBoxHeight = Math.floor(document.documentElement.clientHeight)
		},
		
		setViewboxDebounced() {
			return debounce(this.setViewbox, 200)
		},
		
		animateAll() {
			let vm = this
			const tl = new TimelineMax()
			
			let arc1point = {
				start: vm.arc1.endAngle,
				finish: 360 + vm.arc1.startAngle - .01
				// -0.01 is to not close the arc - it desappears other way
			}
			
			let arc2point = {
				start: vm.arc2.endAngle,
				finish: vm.arc2.startAngle
			}
			
			tl.set(['#c1', '#c2'], { autoAlpha: 0 })
			
			.set('#frontMaskSvg', {autoAlpha: 1})
			
			.set({}, {}, 3) // fake pause before playing
			
			.add('startMotion')
			
			.to(arc1point, 1, {
				start: arc1point.finish,
				ease: Power4.easeInOut,
				onUpdate() {
					Vue.set(vm.arc1, 'endAngle', arc1point.start)
				}
			}, 'startMotion')
			
			.to('#arc1', 0, { autoAlpha: 0 })
			.to('#c1', 0, { autoAlpha: 1 })
			
			.to(arc2point, 1, {
				start: arc2point.finish,
				ease: Power4.easeInOut,
				onUpdate() {
					Vue.set(vm.arc2, 'endAngle', arc2point.start)
				}
			}, 'startMotion')
			.to('#arc2', 0, { autoAlpha: 0 })
			.to('#c2', 0, { autoAlpha: 1 })
			
			.add('outMotion')
			
			.to('#c1', .75, {
				ease: Power4.easeInOut,
				attr: {
					r: 0
				}
			}, 'outMotion')
			.to('#c2', .75, {
				ease: Power4.easeInOut,
				attr: {
					r: vm.outerRad
				}
			}, 'outMotion')
			
			.add('bgMotion')
			
			.fromTo('#title', 1, {
				x: 150
			}, {
				ease: Power2.easeInOut,
				x: 0,
				color: '#fff'
			}, 'outMotion')
			.fromTo('#text', 1, {
				x: -15,
				autoAlpha: 0
			}, {
				ease: Power2.easeInOut,
				x: 0,
				autoAlpha: 1
			}, 'outMotion')
			
			// turn off the whole svg
			.to('#frontMaskSvg', 0, { 
				autoAlpha: 0,
				onComplete() {
					window.removeEventListener('resize', vm.setViewboxDebounced)
				}
			})
			
			.to('#bg', 12, {
				scale: 1,
				ease: Power1.easeOut
			}, 'bgMotion-=.5')
			
		}
	}
})