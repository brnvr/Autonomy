import axios from 'axios';
import DataTable, { DataTableHandle } from './DataTable';
import { createRef, useEffect } from 'react';

const columns = [
  { title: 'Id', minWidth: '100px', render: (data:any) => data.id },
  { title: 'Name', minWidth: '100px', render: (data:any) => data.name },
];

const dataTableRef = createRef<DataTableHandle>();
  
const fetchTable = () => {
  if (dataTableRef.current) {
    dataTableRef.current.fetch()
  }
}

const getRows = (page:number) => {
  return axios.get(`${import.meta.env.VITE_API_BASE_URL}Budgets`, {
    params: {
      page,
      pageLength: 2
    }
  }).then(r => {
    return r.data
  })
}

const BudgetsTable = () => {
  useEffect(() => {
    fetchTable()
  }, []);

  return (
    <>
      <DataTable title="Budgets" columns={columns} callback={getRows} ref={dataTableRef}/>
    </>
  );
};

export default BudgetsTable
