import { send } from "../utilities";

let welcomeDiv = document.getElementById("welcomeDiv") as HTMLDivElement;

if(localStorage.getItem("userId") !=null){
let username =await send("getUsername", localStorage.getItem("userId")) as string;

welcomeDiv.innerText = "welcome " + username +"!";
}
