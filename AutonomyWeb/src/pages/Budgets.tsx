import Breadcrumb from '../components/Breadcrumbs/Breadcrumb';
import BudgetsTable from '../components/Tables/BudgetsTable';

const Budgets = () => {
  return (
    <>
      <Breadcrumb pageName="Budgets" />
      
      <div className="flex flex-col gap-10">
        <BudgetsTable />
      </div>
    </>
  );
};

export default Budgets;
