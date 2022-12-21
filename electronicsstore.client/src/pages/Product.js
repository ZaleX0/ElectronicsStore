import { useEffect, useState } from "react"
import { Button, Card } from "react-bootstrap"
import { Dash, Plus, Trash } from "react-bootstrap-icons"
import { useParams } from "react-router-dom"
import { useShoppingCart } from "../hooks/useShoppingCart"
import StoreService from "../services/StoreService"
import { formatCurrency } from "../utilities/formatCurrency"

export default function Product() {
  const useParamsId = () => {
    const { id } = useParams();
    return parseInt(id);
  }
  const id = useParamsId();
  const {
    getItemQuantity,
    increaseCartQuantity,
    decreaseCartQuantity,
    removeFromCart,
  } = useShoppingCart()
  const quantity = getItemQuantity(id)

  const storeService = new StoreService()
  const [item, setItem] = useState({})
  
  useEffect(() => {
    const fetch = async () => setItem(await storeService.getProductById(id));
    fetch();
  }, [])

  return (
    <>
      <Card>
      <Card.Header>
        <Card.Title>{item.name}</Card.Title>
      </Card.Header>
      <Card.Body className="d-flex justify-content-between">
      <img src={item.imageUrl} alt="" style={{height: "250px", width: "250px", objectFit: "cover"}} />
          <div className="w-50">
            <div className="display-6 mb-4">
              {item.brandName}
            </div>
            <div>
              <p style={{textAlign: "justify"}}>
                {item.description}
              </p>
            </div>
          </div>
          <div>
            <h1 className="display-6">{formatCurrency(item.price)}</h1>
          </div>
      </Card.Body>
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
    </>
  )
}