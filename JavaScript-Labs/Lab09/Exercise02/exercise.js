const first = document.getElementById("first");
const stories = document.getElementById("stories");
const relations = [
  "first → " + first.nodeName,
  "first.firstChild → " + first.firstChild.nodeName,
  "first.firstChild.nextSibling → " + first.firstChild.nextSibling.nodeName,
  "first.parentNode → " + first.parentNode.nodeName,
];
document.getElementById("family-log").textContent = relations.join("\n");

const children = [];
for (const [index, child] of stories.childNodes.entries()) {
  const line = "child " + index + " = " + child.nodeName + ", nodeType = " + child.nodeType;
  console.log(line);
  children.push(line);

  // Figure 9.1 tô các thẻ con của #stories. Các nút khoảng trắng vẫn giữ nguyên.
  if (child.nodeType === Node.ELEMENT_NODE) {
    child.classList.add("element-child");
  }
}
document.getElementById("children-log").textContent = children.join("\n");
