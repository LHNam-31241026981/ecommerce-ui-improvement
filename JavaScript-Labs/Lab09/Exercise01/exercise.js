const messageArea = document.getElementById("msgArea");
const first = document.getElementById("first");
const log = document.getElementById("selector-log");
const toggle = document.getElementById("toggle-message");

function record(label, value) {
  const item = document.createElement("li");
  item.textContent = label + " → " + value;
  log.appendChild(item);
}

messageArea.value = "here is some text";
record('getElementById("msgArea").value', messageArea.value);

const title = document.getElementById("pageTitle");
title.innerHTML = "New Title";
record("title.innerHTML", title.innerHTML);
document.getElementById("pageTitle").innerHTML = "Even newer title";

messageArea.value = first.innerHTML;
record('getElementById("first").innerHTML', first.querySelector("p").outerHTML);
messageArea.value = first.innerText;
record('getElementById("first").innerText', first.querySelector("p").innerText);

messageArea.value = "My class is " + messageArea.getAttribute("class");
record('getAttribute("class")', messageArea.value);

const textareas = document.getElementsByTagName("textarea");
textareas[0].value = "selected by tag name";
record('getElementsByTagName("textarea")', textareas[0].value);
const spans = document.getElementById("features").getElementsByTagName("span");
for (const span of spans) {
  messageArea.value += "\n" + span.innerText;
}
record('getElementsByTagName("span")', messageArea.value);

const counts = document.getElementsByClassName("count-number");
messageArea.value = "";
for (const count of counts) {
  messageArea.value += count.innerText + " ";
}
record('getElementsByClassName("count-number")', messageArea.value);

const firstParagraph = document.querySelector("#first p");
messageArea.value = firstParagraph.innerHTML;
record('querySelector("#first p")', messageArea.value);
const paragraphs = document.querySelectorAll("#first p");
messageArea.value = "";
for (const paragraph of paragraphs) {
  messageArea.value += paragraph.innerHTML + "\n";
}
record('querySelectorAll("#first p")', paragraphs.length + " đoạn văn");

messageArea.setAttribute("class", "hidden");
document.getElementById("features").style.backgroundColor = "green";

toggle.addEventListener("click", () => {
  const show = messageArea.classList.contains("hidden");
  messageArea.classList.toggle("hidden", !show);
  toggle.setAttribute("aria-expanded", String(show));
  toggle.textContent = show ? "Ẩn ô kết quả" : "Hiện ô kết quả";
});
