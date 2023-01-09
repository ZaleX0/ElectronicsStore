import React from 'react'
import { Link } from 'react-router-dom'

export default function Home() {
  return (
    <>
      <h1 className="display-4">Electronics Store</h1>
      <p>
        This project is a simple store application with cart and authorization.
      </p>
      <p>
        See description of the project on
        <a target="_blank" rel="noreferrer" href="https://github.com/ZaleX0/ElectronicsStore">github</a>.
      </p>
      Features:
      <ul>
        <li>JWT Bearer Authorization</li>
        <li>Adding multiple products to cart</li>
        <li>Sorting and filtering store items by brand, category, price etc.</li>
        <li>Pagination</li>
        <li>Checking status of your orders (accepted by admin or not)</li>
        <li>Admin panel where you can accept other users orders</li>
      </ul>
      <p>Go to <Link to="/store">store page</Link> to browse products and to <Link to="login">login page</Link> to sign in</p>
    </>
  )
}
