const tabItemList = document.querySelectorAll('.tab__item');
const tabContent = document.getElementById('tab-content');
const contentNews = document.getElementsByClassName('content-news');

const tabActiveClassName = 'tab__item--active';

const contentList = [
	...contentNews // Convert the node list to an array using the spread operator
];

const onTabClick = (id) => {
	tabItemList.forEach((tab, index) => {
		if (id !== index) {
			tab.classList.remove(tabActiveClassName);
			return;
		}

		tab.classList.add(tabActiveClassName);
	});

	if (contentList[id] !== '') {
		tabContent.innerHTML = contentList[id].innerHTML;
	} else {
		tabContent.innerHTML = 'Đang phát triển';
	}
};