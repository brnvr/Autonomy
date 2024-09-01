import Breadcrumb from '../components/Breadcrumbs/Breadcrumb';
import ServicesTable from '../components/Tables/ServicesTable';

const Services = () => {
  return (
    <>
      <Breadcrumb pageName="Services" />
      
      <div className="flex flex-col gap-10">
        <ServicesTable />
      </div>
    </>
  );
};

export default Services;
