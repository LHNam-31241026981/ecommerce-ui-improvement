const first = document.getElementById("first");
const text = document.createTextNode("this is programmatically created");
const paragraph = document.createElement("p");
paragraph.className = "lab09-feedback";
paragraph.appendChild(text);
first.appendChild(paragraph);

const icons = document.getElementById("iconList");
const item = document.getElementById("deleteThisOne");
const removed = icons.removeChild(item);
document.getElementById("dom-change").textContent =
  'Đã xóa "' + removed.textContent + '". Danh sách còn ' + icons.children.length + " mục.";
