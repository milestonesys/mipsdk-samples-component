(function () {
	
	function onLoad() {
		
		var forms = document.querySelectorAll('form');
		Object.keys(forms).forEach(function (key) {
			forms[key].addEventListener('submit', function () {
				document.body.classList.add('submitted');
				console.log('submitted');
			});
		});

		var elements = document.querySelectorAll('*');
		Object.keys(elements).forEach(function (key) {
			
			var styles = window.getComputedStyle(elements[key], null);
			if (styles.getPropertyValue('overflow-x') == 'auto') {
				new Scrollbar(elements[key], true);
			}
		});
	}

	var Scrollbar = function (element, horizontal) {

		var container = document.createElement('ins');
		var content = document.createElement('ins');
		var handle = document.createElement('ins');
		var animation, offset;

		initialize();

		function initialize() {

			container.classList.add('scrollContainer');
			content.classList.add('scrollContent');
			handle.classList.add('scrollHandle');
			
			horizontal && container.classList.add('horizontal');

			while (element.childNodes.length) {
				content.appendChild(element.firstChild);
			}
			container.appendChild(content);
			container.appendChild(handle);

			element.appendChild(container);

			window.addEventListener('resize', update);

			content.addEventListener('scroll', update);
			content.addEventListener('animationend', update);
			content.addEventListener('transitionend', update);

			handle.addEventListener('mousedown', mouseDown);

			update();
		}
		
		function mouseDown(event) {
			
			event.preventDefault();
			
			var offsets = handle.getBoundingClientRect();
			offset = horizontal ? event.pageX - offsets.left : event.pageY - offsets.top;
			
			handle.classList.add('selected');
			
			window.addEventListener('mousemove', mouseMove);
			window.addEventListener('mouseup', mouseUp);
			
			content.removeEventListener('scroll', update);
			content.removeEventListener('animationend', update);
			content.removeEventListener('transitionend', update);			
		}
		
		function mouseMove(event) {

			event.preventDefault();
			
			if (horizontal) {
				var position = Math.max(0, Math.min(content.offsetWidth - handle.offsetWidth, event.pageX - content.getBoundingClientRect().left - offset));
				handle.style.left = position + 'px';
				content.scrollLeft = position * content.scrollWidth / content.offsetWidth;
			}
			else {
				var position = Math.max(0, Math.min(content.offsetHeight - handle.offsetHeight, event.pageY - content.getBoundingClientRect().top - offset));
				handle.style.top = position + 'px';
				content.scrollTop = position * content.scrollHeight / content.offsetHeight;
			}			
		}

		function mouseUp(event) {

			event.preventDefault();
			
			handle.classList.remove('selected');
			
			window.removeEventListener('mousemove', mouseMove);
			window.removeEventListener('mouseup', mouseUp);
			
			content.addEventListener('scroll', update);
			content.addEventListener('animationend', update);
			content.addEventListener('transitionend', update);
			
			update();
		}

		function update() {

			cancelAnimationFrame(animation);
			animation = requestAnimationFrame(function () {
				if (horizontal) {
					var ratio = content.offsetWidth / content.scrollWidth;
					handle.style.width = ratio * content.offsetWidth + 'px';
					handle.style.left = ratio * content.scrollLeft + 'px';
				}
				else {
					var ratio = content.offsetHeight / content.scrollHeight;
					handle.style.height = ratio * content.offsetHeight + 'px';
					handle.style.top = ratio * content.scrollTop + 'px';
				}
			});
		}
	};
		
	window.addEventListener('load', onLoad);
})();
