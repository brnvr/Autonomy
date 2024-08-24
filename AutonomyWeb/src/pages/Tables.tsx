import Breadcrumb from '../components/Breadcrumbs/Breadcrumb';
import TableOne from '../components/Tables/TableOne';
import TableThree from '../components/Tables/TableThree';
import TableTwo from '../components/Tables/TableTwo';
import DataTable from '../components/Tables/DataTable';
import axios from 'axios'
import { BallTriangle } from 'react-loader-spinner'

const Tables = () => {
  const columns = [
    { title: 'Id', minWidth: '100px', render: (data:any) => data.id },
    { title: 'Name', minWidth: '100px', render: (data:any) => data.name },
  ];

  const getRows = () => {
    return axios.get("http://localhost:81/AutonomyApi/Budgets").then(r => {
      return r.data.selected
    })
  }

  return (
    <>
      <Breadcrumb pageName="Tables" />

      <div className="flex flex-col gap-10">
        <TableOne />
        <TableTwo />
        <TableThree />
        <DataTable columns={columns} fetch={getRows}/>
      </div>
    </>
  );
};

export default Tables;
