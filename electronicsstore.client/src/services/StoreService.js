export default class StoreService {
  
  async getProducts() {
    const response = await fetch("/api/product", {
      method: "GET"
    });
    return await response.json();
  }

  async getProductById(id) {
    const response = await fetch(`/api/product/${id}`, {
      method: "GET"
    });
    return await response.json();
  }
}
