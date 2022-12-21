import React, { useEffect, useState } from 'react'
import { StoreItem } from '../components/store/StoreItem'
import StoreService from '../services/StoreService'

export function Store() {
  const storeService = new StoreService();
  const [storeItems, setStoreItems] = useState([]);

  useEffect(() => {
    const fetchData = async () => setStoreItems(await storeService.getProducts());
    fetchData();
  }, [])
  

  return (
    <>
      <h1 className="display-1">Store</h1>
      <div className="d-flex flex-column">
        {storeItems.map(item =>
          <div key={item.id} className="mb-2">
            <StoreItem {...item} />
          </div>
        )}
      </div>
    </>
  )
}
