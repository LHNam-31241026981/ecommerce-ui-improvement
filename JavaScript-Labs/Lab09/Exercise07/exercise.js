function setBackground(event) {
  event.target.style.backgroundColor = event.type === "focus" ? "#FFE393" : "white";
}

window.addEventListener("load", () => {
  const fields = document.querySelectorAll('input[type="text"], input[type="password"]');
  for (const field of fields) {
    field.addEventListener("focus", setBackground);
    field.addEventListener("blur", setBackground);
  }
});
