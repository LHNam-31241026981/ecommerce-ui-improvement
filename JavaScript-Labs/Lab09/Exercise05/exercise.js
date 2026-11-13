// Script này chạy trong <head>, khi trình duyệt chưa phân tích đến #div1.
const earlyResult = document.getElementById("div1");

window.addEventListener("load", () => {
  const div = document.getElementById("div1");
  document.getElementById("load-result").textContent =
    "Trong <head> → " + earlyResult + "\nSau sự kiện load → " + div.innerHTML;
  alert(div.innerHTML);
});
