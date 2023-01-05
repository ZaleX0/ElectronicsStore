import React, { useEffect } from 'react'
import { Button, Container, Nav, Navbar as NavbarBs } from 'react-bootstrap'
import { NavLink } from 'react-router-dom'
import { useShoppingCart } from '../hooks/useShoppingCart'
import { useAuth } from '../hooks/useAuth'

export function Navbar() {
  const { openCart, cartQuantity } = useShoppingCart();
  const { isLogin, user, login, logout } = useAuth();

  return (
    <>
      {(isLogin && user.roleName === "Admin") && renderAdminBar()}
      <NavbarBs expand="lg" variant="dark" bg="dark" className="shadow mb-4">
        <Container>
          <NavbarBs.Brand to="/" as={NavLink}>Electronics Store</NavbarBs.Brand>
          <NavbarBs.Toggle aria-controls="responsive-navbar-nav" />
          <NavbarBs.Collapse id="responsive-navbar-nav">
            <Nav className="me-auto">
              <Nav.Link to="/" as={NavLink}>Home</Nav.Link>
              <Nav.Link to="/store" as={NavLink}>Store</Nav.Link>
              {isLogin && <Nav.Link to="/orders" as={NavLink}>My Orders</Nav.Link>}
            </Nav>
            <Nav>
              {renderAuthNavLink()}
            </Nav>
            {renderCartButton()}
          </NavbarBs.Collapse>
        </Container>
      </NavbarBs>
    </>
  )

  function renderAdminBar() {
    //if (isLogin && user.roleName === "Admin") 
      return (
        <NavbarBs expand="lg" bg="info">
          <Container>
          <NavbarBs.Brand>Admin Panel</NavbarBs.Brand>
          <NavbarBs.Toggle aria-controls="responsive-navbar-nav" />
          <NavbarBs.Collapse id="responsive-navbar-nav">
            <Nav className="me-auto">
                <Nav.Link to="/orders/admin" as={NavLink}>Orders</Nav.Link>
            </Nav>
          </NavbarBs.Collapse>
          </Container>
        </NavbarBs>
      )
  }

  function renderAuthNavLink() {
    if (isLogin) return <>
      <NavbarBs.Text>Signed as <i>{user.email}</i></NavbarBs.Text>
      <Nav.Link onClick={logout}>Logout</Nav.Link>
    </>

    return <Nav.Link to="/login" as={NavLink}>Sign in</Nav.Link>
  }

  function renderCartButton() {
    return (
      <Button
        onClick={openCart}
        disabled={cartQuantity === 0}
        style={{width: "3rem", height: "3rem", position: "relative"}}
        variant="primary"
        className="rounded-circle ms-2"
      >
        <svg
          xmlns="http://www.w3.org/2000/svg"
          fill="currentColor"
          className="bi bi-cart"
          viewBox="0 0 16 16"
        >
          <path d="M0 1.5A.5.5 0 0 1 .5 1H2a.5.5 0 0 1 .485.379L2.89 3H14.5a.5.5 0 0 1 .491.592l-1.5 8A.5.5 0 0 1 13 12H4a.5.5 0 0 1-.491-.408L2.01 3.607 1.61 2H.5a.5.5 0 0 1-.5-.5zM3.102 4l1.313 7h8.17l1.313-7H3.102zM5 12a2 2 0 1 0 0 4 2 2 0 0 0 0-4zm7 0a2 2 0 1 0 0 4 2 2 0 0 0 0-4zm-7 1a1 1 0 1 1 0 2 1 1 0 0 1 0-2zm7 0a1 1 0 1 1 0 2 1 1 0 0 1 0-2z"/>
        </svg>
        {cartQuantity > 0 && (
          <div
            className="rounded-circle bg-danger d-flex justify-content-center align-items-center"
            style={{
              color: "white",
              width: "1.5rem",
              height: "1.5rem",
              position: "absolute",
              bottom: 0,
              right: 0,
              transform: "translate(25%, 25%)"
            }}
          >
            {cartQuantity}
          </div>
        )}
      </Button>
    )
  }
}
