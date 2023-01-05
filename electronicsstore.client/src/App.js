import 'bootstrap/dist/css/bootstrap.css'
import { Container } from 'react-bootstrap'
import { Route, Routes } from "react-router-dom"
import Login from "./pages/Login"
import Home from "./pages/Home"
import { Navbar } from "./components/Navbar"
import { Store } from './pages/Store'
import { ShoppingCartProvider } from './contexts/ShoppingCartContext'
import Checkout from './pages/Checkout'
import { AuthProvider } from './contexts/AuthContext'
import Product from './pages/Product'
import CheckoutSuccess from './pages/CheckoutSuccess'
import Register from './pages/Register'
import Orders from './pages/Orders'
import OrdersAdmin from './pages/OrdersAdmin'

export default function App() {
  return (
    <AuthProvider>
      <ShoppingCartProvider>
        <Navbar />
        <Container>
          {routes()}
        </Container>
        <footer>
          footer
        </footer>
      </ShoppingCartProvider>
    </AuthProvider>
  )

  function routes() {
    return (
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/store">
          <Route index element={<Store />} />
          <Route path=":id" element={<Product />} />
        </Route>
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/checkout">
          <Route index element={<Checkout />} />
          <Route path="success" element={<CheckoutSuccess />} />
        </Route>
        <Route path="/orders">
          <Route index element={<Orders/>} />
          <Route path="admin" element={<OrdersAdmin />} />
        </Route>
      </Routes>
    )
  }
}
