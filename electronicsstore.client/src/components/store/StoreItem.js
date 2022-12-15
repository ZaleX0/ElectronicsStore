import React from 'react'
import { Button, Card } from 'react-bootstrap'
import { useShoppingCart } from '../../contexts/ShoppingCartContext';

export function StoreItem({id, brandId, name, brandName, description, price}) {
  const {
    getItemQuantity,
    increaseCartQuantity,
    decreaseCartQuantity,
    removeFromCart,
  } = useShoppingCart()
  const quantity = getItemQuantity(id)
  return (
    <Card>
      <Card.Body className="d-flex justify-content-between">
        <div>
          <Card.Title>{name}</Card.Title>
          <ul>
            <li>
              {brandName}
            </li>
            <li>
              {description}
            </li>
          </ul>
        </div>
        <div>
          <h1 className="display-6">${price}</h1>
        </div>
      </Card.Body>
      <Card.Footer className="d-flex justify-content-end" style={{gap: ".5rem"}}>
        {quantity === 0 ? (
          <Button onClick={() => increaseCartQuantity(id)}>Add to cart</Button>
        ) : (
          <>
            <Button onClick={() => decreaseCartQuantity(id)}>-</Button>
            <div>
              <span className="fs-3">{quantity}</span> in cart
            </div>
            <Button onClick={() => increaseCartQuantity(id)}>+</Button>
          </>
        )}
      </Card.Footer>
    </Card>
  )
}