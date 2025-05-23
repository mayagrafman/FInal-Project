import { send } from "../utilities";

let usernameInput = document.getElementById("usernameInput")! as HTMLInputElement;
let passwordInput = document.getElementById("passwordInput")! as HTMLInputElement;
let loginButton = document.getElementById("loginButton")! as HTMLButtonElement;
let messageDiv = document.getElementById("messageDiv") as HTMLDivElement;
let signupButton = document.getElementById("signupButton")! as HTMLButtonElement;
let ratingInput = document.querySelector("#ratingInput") as HTMLInputElement;


loginButton.onclick = async function() {
  let userId = await send("logIn", [usernameInput.value, passwordInput.value]) as string | null;

  if (userId != null) {
    localStorage.setItem("userId", userId);
    location.href = "index.html";
  } else {
    messageDiv.innerText = "This username doesn't exist yet";
    signupButton.style.display = "inline"; // מציגים את הכפתור
  }
};

// מעבירים לעמוד ההרשמה
signupButton.onclick = function() {
  location.href = "signup.html";
};
