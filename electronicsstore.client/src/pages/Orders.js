import React, { useEffect, useState } from 'react'
import { FormSelect, InputGroup, Pagination, Table } from 'react-bootstrap';
import { CheckLg, ThreeDots } from 'react-bootstrap-icons';
import { useNavigate } from 'react-router-dom';
import OrderService from '../services/OrderService'
import { formatCurrency } from '../utilities/formatCurrency';
import { formatDate } from '../utilities/formatDate';

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
      <h1 className="display-4 mb-4">My orders</h1>
      <Table variant="bordered" className="mb-2">
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
            <td>{formatDate(item.timeOrdered)}</td>
            <td>{formatDate(item.timeAccepted)}</td>
            <td>
              {item.products.map(p =>
                <div key={p.id}>
                  <span className="text-muted">x{p.quantity}</span> - {p.name}
                  <br/>
                </div>
              )}
            </td>
            <td>{formatCurrency(item.totalPrice)}</td>
            <td className="text-center align-middle">
              {item.isAccepted
                ? <CheckLg size={50} color="green"/>
                : <ThreeDots size={20} color="blue"/>
              }
            </td>
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
            <option value={20}>20</option>
            <option value={30}>30</option>
          </FormSelect>
        </InputGroup>
      </div>
    )
  }
}