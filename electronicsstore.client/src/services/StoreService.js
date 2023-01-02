export default class StoreService {
  
  async getProducts(searchPhrase, pageNumber, pageSize, sortBy, sortDirection, categoryId) {
    const params = `?searchPhrase=${searchPhrase}&pageNumber=${pageNumber}&pageSize=${pageSize}&sortBy=${sortBy}&sortDirection=${sortDirection}&categoryId=${categoryId}`;
    const response = await fetch(`/api/product${params}`, {
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

  async getCategories() {
    const response = await fetch(`/api/category`, {
      method: "GET"
    });
    return await response.json();
  }
}
