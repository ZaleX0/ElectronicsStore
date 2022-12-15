import 'bootstrap/dist/css/bootstrap.css'
import { Container } from 'react-bootstrap'
import { Route, Routes } from "react-router-dom"
import Login from "./components/auth/Login"
import Home from "./pages/Home"
import { Navbar } from "./components/Navbar"
import { Store } from './pages/Store'
import { ShoppingCartProvider } from './contexts/ShoppingCartContext'

export default function App() {
  return (
    <ShoppingCartProvider>
      <Navbar />
      <Container>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/store" element={<Store />} />
          <Route path="/login" element={<Login />} />
        </Routes>
      </Container>
    </ShoppingCartProvider>
  )
}
