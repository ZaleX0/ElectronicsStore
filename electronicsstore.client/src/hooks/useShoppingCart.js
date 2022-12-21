import { useContext } from "react";
import { ShoppingCartContext } from "../contexts/ShoppingCartContext";

export function useShoppingCart() {
  return useContext(ShoppingCartContext)
}