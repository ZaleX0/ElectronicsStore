import React, { useEffect, useState } from 'react'
import { FormSelect, InputGroup, Pagination, Table } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import OrderService from '../services/OrderService'

export default function Orders() {
  const orderService = new OrderService();
  const navigate = useNavigate();
  const [orderPage, setOrderPage] = useState([]);
  const [orderItems, setOrderItems] = useState([]);

  const [currentPage, setCurrentPage] = useState(1);
  const [itemsPerPage, setItemsPerPage] = useState(10);

  useEffect(() => {
    const fetchData = async () => {
      const response = await orderService.getUserOrders(currentPage, itemsPerPage);
      if (response.status === 200) {
        setOrderPage(JSON.parse(response.text));
        setOrderItems(JSON.parse(response.text).items);
      } else {
        navigate("/login");
      }
    }
    fetchData();
  }, [currentPage, itemsPerPage])
  
  return (
    <>
      <h1 className="display-4">My orders</h1>
      <Table variant="bordered">
        <thead className="table-primary">
          <tr>
            <th>Id</th>
            <th>Time ordered</th>
            <th>Time accepted</th>
            <th>Products</th>
            <th>Total price</th>
            <th>Accepted</th>
          </tr>
        </thead>
        <tbody>
        {orderItems.map(item =>
          <tr key={item.id}>
            <td>{item.id}</td>
            <td>{item.timeOrdered}</td>
            <td>{item.timeAccepted}</td>
            <td>
              {item.products.map(p =>
                <>
                  {p.name}
                  <br/>
                </>
              )}
            </td>
            <td>{item.totalPrice}</td>
            <td>{item.isAccepted ? "true" : "false"}</td>
          </tr>
        )}
        </tbody>
      </Table>
      {renderPagination()}
    </>
  )

  function renderPagination() {
    const handleItemsPerPageChange = (event) => {
      setItemsPerPage(event.target.value);
      setCurrentPage(1);
    }

    let paginationItems = [];
    for (let i = 1; i <= orderPage.totalPages; i++) {
      paginationItems.push(
        <Pagination.Item key={i} active={i === currentPage} onClick={() => setCurrentPage(i)}>
          {i}
        </Pagination.Item>
      );
    }
    return (
      <div className="d-flex justify-content-between">
        <Pagination className="m-0">
          {paginationItems}
        </Pagination>
        <InputGroup className="ms-2 w-auto">
          <InputGroup.Text>Orders per page</InputGroup.Text>
          <FormSelect value={itemsPerPage} onChange={handleItemsPerPageChange}>
            <option value={2}>2</option>
            <option value={5}>5</option>
            <option value={10}>10</option>
            <option value={15}>15</option>
          </FormSelect>
        </InputGroup>
      </div>
    )
  }
}