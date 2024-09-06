import axios from 'axios'
import { useState } from 'react'
import { useForm } from 'react-hook-form'
import TextAreaField from '../Form/TextAreaField'
import InputField from '../Form/InputField'
import TableBase, { FormState } from './TableBase'
import Button from '../Buttons/Button'
import { BiNotepad } from 'react-icons/bi'
import { getTableInfo } from '../../language-utils'
import BudgetsModal from '../Modals/BudgetsModal'
import toast, { Toaster } from 'react-hot-toast'
import { handleErrors } from '../../ui-utils'

type Inputs = {
  name: string
  description: string
}

const services = getTableInfo("en", "services")

const updateServiceBudgetTempalte = (id:number, data:any) => {
  return axios.put(`${import.meta.env.VITE_API_BASE_URL}Services/${id}/BudgetTemplate`, data).then(r => r.data)
}

const getServiceById = (id:number) => {
  return axios.get(`${import.meta.env.VITE_API_BASE_URL}Services/${id}`).then(r => r.data)
}

const getServices = (page:number, searchTerm?:string) => {
  return axios.get(`${import.meta.env.VITE_API_BASE_URL}Services`, {
    params: {
      page,
      pageLength: 10,
      order: "id",
      nameOrDescription: searchTerm
    }
  }).then(r => {
    return r.data
  })
}

const updateService = (id:number, data:any) => {
  return axios.put(`${import.meta.env.VITE_API_BASE_URL}Services/${id}`, data).then(r => {
    return r.data
  })
}

const createService = (data:any) => {
  return axios.post(`${import.meta.env.VITE_API_BASE_URL}Services`, data).then(r => {
    return r.data
  })
}

const deleteService = (id:number) => {
  return axios.delete(`${import.meta.env.VITE_API_BASE_URL}Services/${id}`).then(r => {
    return r.data
  })
}

const columns = [
  { title: 'Name', minWidth: '100px', render: (data:any) => data.name },
  { title: 'Description', minWidth: '100px', render: (data:any) => data.description }
]

const ServicesTable = () => {
  const [formData, setFormData] = useState<any>(null)
  const [formState, setFormState] = useState<FormState>(null)
  const [showModalBudget, setShowModalBudget] = useState<boolean>(false)
  
  const {
    register,
    setValue,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm<Inputs>()

  const formContent = <>
    <InputField
      label={services.form.name.label}
      name="name"
      defaultValue={formData?.name}
      placeholder={services.form.name.placeholder}
      register={register}
      errors={errors}
      setValue={setValue}
      options={{required:true, minLength:2}}
    />
    <TextAreaField
      label={services.form.description.label}
      name="description"
      defaultValue={formData?.description}
      placeholder={services.form.description.placeholder}
      register={register}
      errors={errors}
      setValue={setValue}
    />
    <div className="form-field">
      {formData && <Button
        content={<>
          <BiNotepad />
            <span>{services.form.budgetTemplate.buttonLabel}</span>
          </>}
        color={'blue'}
        border={true}
        disabled={!formData}
        onClick={() => setShowModalBudget(true)}
        animation={formState == "creating" ? "expand-height" : null}
      />}
    </div>
  </>

  return <>
    <TableBase 
      title="Services"
      idProperty="id"
      nameProperty="name"
      resourceName={services.name}
      columns={columns}
      formContent={formContent}
      formInactive={showModalBudget}
      setFormState={setFormState}
      formData={formData}
      setFormData={setFormData}
      handleFormSubmit={handleSubmit}
      resetForm={reset}
      get={getServices}
      getById={getServiceById}
      post={createService}
      update={updateService}
      delete={deleteService}
    />
    <BudgetsModal
      visible = {showModalBudget}
      onSubmit = {data => updateServiceBudgetTempalte(formData.id, data)
        .then(() => toast.success("Budget template updated"))
        .catch(r => handleErrors(r, ""))
      }
      onClose = {()=>setShowModalBudget(false)}
    />
    <Toaster
      position="bottom-right"
    />
  </>
};

export default ServicesTable
