import { Button, Card } from "react-bootstrap";
import { useShoppingCart } from "../hooks/useShoppingCart";
import OrderService from "../services/OrderService";
import { useEffect } from "react";
import { CheckoutItem } from "../components/cart/CheckoutItem";
import { formatCurrency } from "../utilities/formatCurrency";
import { Bag } from "react-bootstrap-icons";
import { useNavigate } from "react-router-dom";

export default function Checkout() {
  const orderService = new OrderService();
  const { cartItems, cartQuantity, fetchByIds, fetchedItems, clearCart } = useShoppingCart()
  const navigate = useNavigate();

  const checkout = async () => {
    const response = await orderService.makeOrder(cartItems);
    if (response.status === 200) {
      clearCart();
      navigate("/checkout/success");
    }
    if (response.status === 401) {
      navigate("/login");
    }
  };

  useEffect(() => {
    fetchByIds();
  }, [])
  
  return (
    <>
      <h1 className="display-4">Checkout</h1>
      <div className="d-flex">
        <div className="d-flex flex-column w-100">
          {fetchedItems.map(item => {
            return (
              <div key={item.id} className="mb-2">
                <CheckoutItem {...item} />
              </div>
            )
          })}
        </div>
        <Card className="w-25 mb-2 ms-2">
          <Card.Body className="d-flex align-items-end">
            <div className="ms-auto fw-bold fs-5">
              Total {formatCurrency(cartItems.reduce((total, cartItem) => {
                const item = fetchedItems.find(i => i.id === cartItem.id)
                return total + (item?.price || 0) * cartItem.quantity
              }, 0))}
            </div>
          </Card.Body>
          <Card.Footer>
            <Button className="w-100" variant="success" onClick={checkout} disabled={cartQuantity <= 0}>Purchase <Bag /></Button>
          </Card.Footer>
        </Card>
      </div>
    </>
  )
}