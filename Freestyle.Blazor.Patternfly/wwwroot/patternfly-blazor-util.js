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
	updatePopper(element) {
		element.popper.update();
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
	replaceInto(source, destination) {
		// Remove any existing nodes in destination
		while (destination.firstChild) {
			destination.removeChild(destination.lastChild);
		}

		// Add new node as only child
		destination.appendChild(source);
	},
	addOutsideClickHandler: function (element, dotnetHelper, specificTarget) {
		var shouldInvoke = false;
		element.outsideClick = {
			mousedown: (e) => {
				if (specificTarget !== undefined && specificTarget != null && specificTarget != e.target && !specificTarget.contains(e.target)) return;

				if (element.contains) {
					if (!element.contains(e.target)) {
						shouldInvoke = true;
					} else {
						shouldInvoke = false;
					}
				}
			},
			mouseup: (e) => {
				if (specificTarget !== undefined && specificTarget != null && specificTarget != e.target && !specificTarget.contains(e.target)) return;

				if (element.contains) {
					if (!element.contains(e.target)) {
						shouldInvoke = shouldInvoke && true;
					} else {
						shouldInvoke = false;
					}
				}
			},
			click: (e) => {
				if (specificTarget !== undefined && specificTarget != null && specificTarget != e.target && !specificTarget.contains(e.target)) return;

				if (element.contains) {
					if (!element.contains(e.target) && shouldInvoke) {
						dotnetHelper.invokeMethodAsync('InvokeClickOutsideAsync');
					}
				}
			}
		};

		window.addEventListener("mousedown", element.outsideClick.mousedown);
		window.addEventListener("mouseup", element.outsideClick.mouseup);
		window.addEventListener("click", element.outsideClick.click);
	},
	removeOutsideClickHandler: function (element) {
		if (element && element.outsideClick) {
			window.removeEventListener("mousedown", element.outsideClick.mousedown);
			window.removeEventListener("mouseup", element.outsideClick.mouseup);
			window.removeEventListener("click", element.outsideClick.click);
		}
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
			var leftScrollButton = scrollButtons[0];
			var rightScrollButton = scrollButtons[1];

			var lastItemVisible = function () {
				var navItems = navList.getElementsByClassName("pf-c-nav__item");
				if (navItems.length == 0) return true;

				var lastItem = navItems[navItems.length - 1];

				var lastItemRightPosition = lastItem.offsetLeft + lastItem.clientWidth;
				var containerVisibleRight = navList.scrollLeft + navList.clientWidth;
				return lastItemRightPosition <= containerVisibleRight;
			}

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

				var navItems = navList.getElementsByClassName("pf-c-nav__item");
				if (navItems.length == 0) return;

				if (indexToShow > 0) indexToShow--;
				var itemLeft = navItems[indexToShow].offsetLeft;

				navList.scrollLeft = itemLeft;

				setScrollButtonsState();
			});

			rightScrollButton.addEventListener("click", function (e) {
				e.preventDefault();

				var navItems = navList.getElementsByClassName("pf-c-nav__item");
				if (navItems.length == 0) return;

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
			addRemoveScrollable();
		}
	},
	bindTabsScroller(tabsElement) {
		var tabsList = tabsElement.getElementsByClassName("pf-c-tabs__list");
		if (tabsList.length == 0) return;

		tabsList = tabsList[0];

		var scrollButtons = tabsElement.getElementsByClassName("pf-c-tabs__scroll-button");
		if (scrollButtons.length == 2) {
			var indexToShow = 0;
			var leftScrollButton = scrollButtons[0];
			var rightScrollButton = scrollButtons[1];

			var lastItemVisible = function () {
				var tabsItems = tabsList.getElementsByClassName("pf-c-tabs__item");
				if (tabsItems.length == 0) return true;

				var lastItem = tabsItems[tabsItems.length - 1];

				var lastItemRightPosition = lastItem.offsetLeft + lastItem.clientWidth;
				var containerVisibleRight = tabsList.scrollLeft + tabsList.clientWidth;
				return lastItemRightPosition <= containerVisibleRight;
			}

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

				var tabsItems = tabsList.getElementsByClassName("pf-c-tabs__item");
				if (tabsItems.length == 0) return;

				if (indexToShow > 0) indexToShow--;
				var itemLeft = tabsItems[indexToShow].offsetLeft;

				tabsList.scrollLeft = itemLeft;

				setScrollButtonsState();
			});

			rightScrollButton.addEventListener("click", function (e) {
				e.preventDefault();

				var tabsItems = tabsList.getElementsByClassName("pf-c-tabs__item");
				if (tabsItems.length == 0) return;

				if (indexToShow < tabsItems.length - 1) indexToShow++;
				var itemLeft = tabsItems[indexToShow].offsetLeft;

				tabsList.scrollLeft = itemLeft;

				setScrollButtonsState();
			});

			var addRemoveScrollable = function () {
				if (tabsList.scrollWidth > tabsList.clientWidth) {
					if (!tabsElement.classList.contains("pf-m-scrollable"))
						tabsElement.classList.add("pf-m-scrollable");
				} else {
					if (tabsElement.classList.contains("pf-m-scrollable"))
						tabsElement.classList.remove("pf-m-scrollable");
				}

				setScrollButtonsState();

				setTimeout(addRemoveScrollable, 250);
			}

			setTimeout(addRemoveScrollable, 250);
			addRemoveScrollable();
		}
	},
	moveCursorToEnd(textElement) {
		textElement.setSelectionRange(textElement.value.length, textElement.value.length);
	}
}

window.patternflyBlazorFunctions.registerDocumentEventListeners();