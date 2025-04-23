import { send } from "../utilities";

let usernameInput = document.getElementById("usernameInput")! as HTMLInputElement;
let passwordInput = document.getElementById("passwordInput")! as HTMLInputElement;
let loginButton = document.getElementById("loginButton")! as HTMLButtonElement;

loginButton.onclick = async function() {

   let userId = await send("logIn", [usernameInput.value, passwordInput.value]) as string | null;

if(userId != null){
   localStorage.setItem("userId", userId);
   location.href = "index.html";

}

};

