export default class AuthService {

  async register(email, password) {
    const response = await fetch("/api/authorization/register", {
      method: "POST",
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({
        email: email,
        password: password
      })
    });
    return { status: response.status, text: await response.text()};
  }

  async login(email, password) {
    const response = await fetch("/api/authorization/login", {
      method: "POST",
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({
        email: email,
        password: password
      })
    });
    return { status: response.status, text: await response.text()};
  }
}
