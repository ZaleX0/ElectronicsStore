import { authHeader } from "../utilities/authHeader";

export default class OrderService {
  async makeOrder(cartItems) {
    const response = await fetch("/api/order", {
      method: "POST",
      headers: {
        'Authorization': authHeader(),
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(cartItems)
    });
    return { status: response.status, text: await response.text() }
  }

  async getUserOrders(pageNumber, pageSize) {
    const params = `?pageNumber=${pageNumber}&pageSize=${pageSize}`;
    const response = await fetch(`/api/order${params}`, {
      method: "GET",
      headers: {
        'Authorization': authHeader(),
        'Content-Type': 'application/json',
      }
    });
    return { status: response.status, text: await response.text() }
  }

  async getAllOrders(showNotAccepted, searchPhrase, pageNumber, pageSize) {
    const params = `?showNotAccepted=${showNotAccepted}&searchPhrase=${searchPhrase}&pageNumber=${pageNumber}&pageSize=${pageSize}`;
    const response = await fetch(`/api/order/all${params}`, {
      method: "GET",
      headers: {
        'Authorization': authHeader(),
        'Content-Type': 'application/json',
      }
    });
    return { status: response.status, text: await response.text() }
  }

  async acceptOrder(id) {
    await fetch(`/api/order/${id}`, {
      method: "PATCH",
      headers: {
        'Authorization': authHeader()
      }
    })
  }
}
