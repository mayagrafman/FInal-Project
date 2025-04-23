import { send } from "../utilities";

export async function getUserId() {
  let userId = localStorage.getItem("userId");

  if (userId == null) {
    return null;
  }

  console.log("userId: " + userId);
  let varified = await send("verifyUserId", userId);
  console.log("varified: " + varified);

  if (!varified) {
    localStorage.removeItem("userId");
    return null;
  }

  return userId;
}
