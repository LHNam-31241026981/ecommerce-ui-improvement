function assignBoxWithoutDeclaration() {
  // Cố ý lược bỏ var để quan sát hành vi của classic script ngoài strict mode.
  box = outputBox(5);
}

document.getElementById("test-scope").addEventListener("click", function () {
  const result = document.getElementById("scope-result");
  let localMessage;

  try {
    console.log(box);
  } catch (error) {
    localMessage = "Có var: " + error.name + " — " + error.message;
    console.log(localMessage);
  }

  assignBoxWithoutDeclaration();
  console.log(box);

  result.hidden = false;
  result.textContent = localMessage + "\nBỏ var: " + window.box;
  delete window.box;
});
