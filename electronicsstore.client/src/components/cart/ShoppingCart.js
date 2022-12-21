import { Button, Offcanvas, Stack } from 'react-bootstrap'
import { Link } from 'react-router-dom'
import { useShoppingCart } from '../../hooks/useShoppingCart'
import { formatCurrency } from '../../utilities/formatCurrency'
import { CartItem } from './CartItem'

export function ShoppingCart({ isOpen }) {
  const { closeCart, cartItems, fetchByIds, fetchedItems } = useShoppingCart()
  return (
    <Offcanvas placement="end" onHide={closeCart} onShow={fetchByIds} show={isOpen}>
      <Offcanvas.Header closeButton>
        <Offcanvas.Title>Cart</Offcanvas.Title>
      </Offcanvas.Header>
      <Offcanvas.Body>
        <Stack gap={3}>
          {fetchedItems.map(item => {
            const props = {...item, ...cartItems.find(i => i.id === item.id)}
            return <CartItem key={item.id} {...props} />
          })}
          <div className="ms-auto fw-bold fs-5">
            Total {formatCurrency(cartItems.reduce((total, cartItem) => {
              const item = fetchedItems.find(i => i.id === cartItem.id)
              return total + (item?.price || 0) * cartItem.quantity
            }, 0))}
          </div>
          <Link to="/checkout" onClick={closeCart}>
            <Button variant="success" className="w-100">
              Checkout
            </Button>
          </Link>
        </Stack>
      </Offcanvas.Body>
    </Offcanvas>
  )
}
