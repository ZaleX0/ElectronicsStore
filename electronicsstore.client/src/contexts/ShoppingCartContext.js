import { createContext, useState } from "react"
import { ShoppingCart } from "../components/cart/ShoppingCart"
import { useLocalStorage } from "../hooks/useLocalStorage"
import StoreService from "../services/StoreService"

export const ShoppingCartContext = createContext({})

export function ShoppingCartProvider( { children } ) {
  const storeService = new StoreService()
  const [isOpen, setIsOpen] = useState(false)
  const [cartItems, setCartItems] = useLocalStorage("shopping-cart", [])
  const [fetchedItems, setFetchedItems] = useState([])
  const cartQuantity = cartItems.reduce((quantity, item) => item.quantity + quantity, 0)

  const openCart = () => setIsOpen(true)
  const closeCart = () => setIsOpen(false)

  const fetchByIds = async () => {
    let requests = [];
    for (let item of cartItems) {
      requests.push(await storeService.getProductById(item.id));
    }
    setFetchedItems(requests);
  }

  function getItemQuantity(id) {
    return cartItems.find(item => item.id === id)?.quantity || 0
  }
  function increaseCartQuantity(id) {
    setCartItems(currItems => {
      if (currItems.find(item => item.id === id) == null) {
        return [...currItems, { id, quantity: 1 }]
      } else {
        return currItems.map(item => {
          if (item.id === id) {
            return { ...item, quantity: item.quantity + 1 }
          } else {
            return item
          }
        })
      }
    })
  }
  function decreaseCartQuantity(id) {
    setCartItems(currItems => {
      if (currItems.find(item => item.id === id)?.quantity === 1) {
        return currItems.filter(item => item.id !== id)
      } else {
        return currItems.map(item => {
          if (item.id === id) {
            return { ...item, quantity: item.quantity - 1 }
          } else {
            return item
          }
        })
      }
    })
    setFetchedItems(currItems => {
      if (currItems.find(item => item.id === id)?.quantity === 1) {
        return currItems.filter(item => item.id !== id)
      }
    })
  }
  function removeFromCart(id) {
    setCartItems(currItems => {
      return currItems.filter(item => item.id !== id)
    })
    setFetchedItems(currItems => {
      return currItems.filter(item => item.id !== id)
    })
  }
  function clearCart() {
    setCartItems([]);
  }

  return (
    <ShoppingCartContext.Provider value={{
      getItemQuantity,
      increaseCartQuantity,
      decreaseCartQuantity,
      removeFromCart,
      clearCart,
      openCart,
      closeCart,
      cartItems,
      cartQuantity,
      fetchByIds,
      fetchedItems
    }}>
      {children}
      <ShoppingCart isOpen={isOpen} />
    </ShoppingCartContext.Provider>
  )
}