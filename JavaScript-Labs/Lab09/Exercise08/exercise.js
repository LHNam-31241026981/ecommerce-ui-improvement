function init() {
  document.getElementById("sampleForm").addEventListener("submit", checkForEmptyFields);
}

function checkForEmptyFields(event) {
  const fields = event.currentTarget.querySelectorAll('input[type="text"], input[type="password"]');
  const errors = document.getElementById("errors");
  const emptyFields = [];

  errors.classList.add("hidden");
  errors.textContent = "";

  for (const field of fields) {
    if (field.value.trim() === "") {
      emptyFields.push(field);
      field.setAttribute("aria-invalid", "true");
      field.setAttribute("aria-describedby", "errors");
    } else {
      field.removeAttribute("aria-invalid");
      field.removeAttribute("aria-describedby");
    }
  }

  if (emptyFields.length > 0) {
    event.preventDefault();
    const fieldNames = emptyFields.map((field) => field.labels[0].textContent.trim());
    errors.textContent = "Các trường chưa nhập: " + fieldNames.join(", ") + ".";
    errors.classList.remove("hidden");
    emptyFields[0].focus();
  }
  // Các ô demo không có thuộc tính name, nên GET không đính kèm giá trị đã nhập.
}

window.addEventListener("load", init);
