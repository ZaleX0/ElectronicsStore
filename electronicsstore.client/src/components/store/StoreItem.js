import React from 'react'
import { Button, Card } from 'react-bootstrap'
import { Dash, Plus, Trash } from 'react-bootstrap-icons';
import { formatCurrency } from '../../utilities/formatCurrency';
import { useShoppingCart } from '../../hooks/useShoppingCart'
import { LinkContainer } from 'react-router-bootstrap';
import './StoreItem.css'

export function StoreItem({id, brandId, categoryId, name, brandName, categoryName, description, imageUrl, price}) {
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
          <img src={imageUrl} alt="" style={{height: "250px", width: "250px", objectFit: "cover"}} />
          <div className="w-50">
            <div className="d-flex justify-content-between align-items-center mb-4">
              <div className="display-6">{brandName}</div>
              <div>{categoryName}</div>
            </div>
            <div>
              <p className="line-clamp">
                {description}
              </p>
            </div>
          </div>
          <div>
            <h1 className="display-6">{formatCurrency(price)}</h1>
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