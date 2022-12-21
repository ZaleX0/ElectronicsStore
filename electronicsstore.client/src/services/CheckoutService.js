import { authHeader } from "../utilities/authHeader";

export default class CheckoutService {
  async checkout(cartItems) {
    const response = await fetch("/api/checkout", {
      method: "POST",
      headers: {
        'Authorization': authHeader(),
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(cartItems)
    });
    return { status: response.status, text: await response.text()}
  }
}
