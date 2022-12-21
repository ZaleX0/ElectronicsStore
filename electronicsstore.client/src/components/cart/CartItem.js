import { Button, Stack } from 'react-bootstrap'
import { Trash } from 'react-bootstrap-icons'
import { useShoppingCart } from '../../hooks/useShoppingCart'
import { formatCurrency } from '../../utilities/formatCurrency'

export function CartItem({ id, quantity, price, imageUrl, name }) {
  const { removeFromCart } = useShoppingCart()
  return (
    <Stack direction="horizontal" gap={2} className="d-flex align-items-center">
      <img src={imageUrl} alt=""
        style={{width:"125px", height: "125px", objectFit: "cover"}} />
        <div className="me-auto">
          <div>
            {name}{" "}
          </div>
          <div className="text-muted" style={{ fontSize: ".75rem" }}>
            {formatCurrency(price)}
            {quantity > 1 && <>{" "}x{quantity}</>}
          </div>
        </div>
        <div>
          {formatCurrency(price * quantity)}
        </div>
        <Button variant="outline-danger" size="sm" onClick={() => removeFromCart(id)}>
          <Trash />
        </Button>
    </Stack>
  )
}
