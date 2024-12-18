import { send } from "../utilities";


let d = document.getElementById("d")!;
let b = document.getElementById("b")!;
let a = document.getElementById("a")!;

let count = await send("setCounter", 0) as string;
a.innerText = count;

d.onclick = async function () {
    let num = await send("setCounter", 1) as string;
    a.innerText = num;
    
}

b.onclick = async function () {
    let num = await send("setCounter",-1) as string;
    a.innerText = num;
    
}