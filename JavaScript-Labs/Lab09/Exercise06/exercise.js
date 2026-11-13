function showEvent(e) {
  const message = "triggered by " + e.target.id;
  document.getElementById("debug-result").textContent =
    e.type + " → " + message;
  alert(message);
}

window.addEventListener("load", () => {
  const boxes = document.querySelectorAll(".movingDiv");
  for (const box of boxes) {
    box.addEventListener("mouseover", showEvent);
    box.addEventListener("click", (e) => {
      // Enter và Space tạo một click không đến từ thiết bị trỏ.
      if (e.detail === 0) {
        showEvent(e);
      }
    });
  }
});
