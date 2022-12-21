import React from 'react'
import { Button, Card } from 'react-bootstrap'
import { Dash, Plus, Trash } from 'react-bootstrap-icons';
import { formatCurrency } from '../../utilities/formatCurrency';
import { useShoppingCart } from '../../hooks/useShoppingCart'
import { LinkContainer } from 'react-router-bootstrap';

export function CheckoutItem({id, brandId, name, brandName, description, imageUrl, price}) {
  const {
    getItemQuantity,
    increaseCartQuantity,
    decreaseCartQuantity,
    removeFromCart,
  } = useShoppingCart()
  const quantity = getItemQuantity(id)
  return (
      <Card>
        <Card.Header>
          <Card.Title>{name}</Card.Title>
        </Card.Header>
        <LinkContainer to={`/store/${id}`} style={{ cursor: "pointer" }}>
          <Card.Body className="d-flex justify-content-between">
            <div className="d-flex">
              <img src={imageUrl} alt="" style={{height: "125px", width: "125px", objectFit: "cover"}} />
              <div className="display-6">
                {brandName}
              </div>
            </div>
            <div>
              <h1 className="display-6">{formatCurrency(price * quantity)}</h1>
              <div className="text-muted">
                {quantity > 1 && <>{formatCurrency(price)} x{quantity}</>}
              </div>
            </div>
          </Card.Body>
        </LinkContainer>
        <Card.Footer className="d-flex justify-content-end" style={{gap: ".5rem"}}>
          {quantity === 0 ? (
            <Button onClick={() => increaseCartQuantity(id)}>Add to cart</Button>
          ) : (
            <>
              <Button onClick={() => removeFromCart(id)} variant="danger"><Trash /></Button>
              <Button onClick={() => decreaseCartQuantity(id)}><Dash /></Button>
              <div>
                <span className="fs-3">{quantity}</span> in cart
              </div>
              <Button onClick={() => increaseCartQuantity(id)}><Plus /></Button>
            </>
          )}
        </Card.Footer>
      </Card>
  )
}