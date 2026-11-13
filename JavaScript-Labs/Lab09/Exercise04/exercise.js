const button = document.getElementById("testButton");
const image = document.getElementById("mainImage");
const imageButton = document.getElementById("imageButton");
const content = document.getElementById("content");
const status = document.getElementById("event-status");
let transitionTimer;

function simpleHandler() {
  clearTimeout(transitionTimer);
  const hide = button.textContent === "Hide";
  button.textContent = hide ? "Show" : "Hide";
  button.setAttribute("aria-expanded", String(!hide));

  if (hide) {
    content.className = "makeItDisappear";
    transitionTimer = setTimeout(() => {
      content.style.display = "none";
      status.textContent = "Đoạn văn đã ẩn.";
    }, 1000);
  } else {
    content.style.display = "block";
    transitionTimer = setTimeout(() => {
      content.className = "makeItNormal";
      status.textContent = "Đang hiển thị đoạn văn.";
    }, 500);
  }
}

function setGray(gray) {
  image.className = gray ? "makeItGray" : "makeItNormal";
  imageButton.setAttribute("aria-pressed", String(gray));
}

button.addEventListener("click", simpleHandler);
image.addEventListener("mouseover", () => setGray(true));
image.addEventListener("mouseout", () => setGray(false));
imageButton.addEventListener("click", () => setGray(!image.classList.contains("makeItGray")));
