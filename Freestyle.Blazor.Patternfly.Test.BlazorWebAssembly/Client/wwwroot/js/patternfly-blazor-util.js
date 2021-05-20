window.patternflyBlazorFunctions = {
	blur: function (element) {
		element.blur();
	},
	clipboardWriteText(text) {
		return navigator.clipboard.writeText(text);
	},
	showPopper(element, tooltip, placement) {
		var popper = Popper.createPopper(element, tooltip, {
			placement: placement,
			modifiers: [
				{
					name: 'offset',
					options: {
						offset: [0, 15],
					},
				},
			],
		});
		element.popper = popper;
		element.setAttribute('aria-describedby', tooltip.id);
		tooltip.removeAttribute('hidden');
	},
	hidePopper(element) {
		if (element.popper) {
			element.popper.destroy();
			element.popper = null;
		}
		element.removeAttribute('aria-describedby');
		tooltip.setAttribute('hidden', '');
	},
	getContentEditableInnerHtml(contentEditable) {
		return contentEditable.innerHTML;
	},
	getContentEditableInnerText(contentEditable) {
		return contentEditable.innerText;
	},
	registerDocumentEventListeners() {
		document.addEventListener('keyup', function (e) {
			if (e.key === "Escape") {
				DotNet.invokeMethodAsync('patternfly-blazor', 'DocumentEventsEscapePressed')
			}
		});
	},
	moveToBodyEnd(element) {
		document.getElementsByTagName('body')[0].appendChild(element);
	},
	addOutsideClickHandler: function (element, dotnetHelper, specificTarget) {
		var shouldInvoke = false;
		window.addEventListener("mousedown", (e) => {
			if (specificTarget != null && specificTarget != e.target && !specificTarget.contains(e.target)) return;

			if (!element.contains(e.target)) {
				shouldInvoke = true;
			} else {
				shouldInvoke = false;
			}
		});
		window.addEventListener("mouseup", (e) => {
			if (specificTarget != null && specificTarget != e.target && !specificTarget.contains(e.target)) return;

			if (!element.contains(e.target)) {
				shouldInvoke = shouldInvoke && true;
			} else {
				shouldInvoke = false;
			}
		});
		window.addEventListener("click", (e) => {
			if (specificTarget != null && specificTarget != e.target && !specificTarget.contains(e.target)) return;

			if (!element.contains(e.target) && shouldInvoke) {
				dotnetHelper.invokeMethodAsync('InvokeClickOutsideAsync');
			}
		});
	},
	addBodyClass(className) {
		document.getElementsByTagName('body')[0].classList.add(className);
	},
	removeBodyClass(className) {
		document.getElementsByTagName('body')[0].classList.remove(className);
	},
	bindNavScroller(navElement) {
		var navList = navElement.getElementsByClassName("pf-c-nav__list");
		if (navList.length == 0) return;

		navList = navList[0];

		var scrollButtons = navElement.getElementsByClassName("pf-c-nav__scroll-button");
		if (scrollButtons.length == 2) {
			var indexToShow = 0;
			var navItems = navList.getElementsByClassName("pf-c-nav__item");
			var lastItem = navItems[navItems.length - 1];

			var lastItemVisible = function () {
				var lastItemRightPosition = lastItem.offsetLeft + lastItem.clientWidth;
				var containerVisibleRight = navList.scrollLeft + navList.clientWidth;
				return lastItemRightPosition <= containerVisibleRight;
			}

			var leftScrollButton = scrollButtons[0];
			var rightScrollButton = scrollButtons[1];

			var setScrollButtonsState = function () {
				if (indexToShow == 0) {
					leftScrollButton.setAttribute('disabled', 'disabled');
				} else {
					leftScrollButton.removeAttribute('disabled');
				}

				if (lastItemVisible()) {
					rightScrollButton.setAttribute('disabled', 'disabled');
				} else {
					rightScrollButton.removeAttribute('disabled');
				}
			}

			leftScrollButton.addEventListener("click", function (e) {
				e.preventDefault();

				if (indexToShow > 0) indexToShow--;
				var itemLeft = navItems[indexToShow].offsetLeft;

				navList.scrollLeft = itemLeft;

				setScrollButtonsState();
			});

			rightScrollButton.addEventListener("click", function (e) {
				e.preventDefault();

				if (indexToShow < navItems.length - 1) indexToShow++;
				var itemLeft = navItems[indexToShow].offsetLeft;

				navList.scrollLeft = itemLeft;

				setScrollButtonsState();
			});

			var addRemoveScrollable = function () {
				if (navList.scrollWidth > navList.clientWidth) {
					if (!navElement.classList.contains("pf-m-scrollable"))
						navElement.classList.add("pf-m-scrollable");
				} else {
					if (navElement.classList.contains("pf-m-scrollable"))
						navElement.classList.remove("pf-m-scrollable");
				}

				setScrollButtonsState();

				setTimeout(addRemoveScrollable, 250);
			}

			setTimeout(addRemoveScrollable, 250);
			//navElement.addEventListener("resize", addRemoveScrollable);
			addRemoveScrollable();
		}
	},
	moveCursorToEnd(textElement) {
		textElement.setSelectionRange(textElement.value.length, textElement.value.length);
	}
}

window.patternflyBlazorFunctions.registerDocumentEventListeners();