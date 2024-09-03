import axios from 'axios'
import { useState } from 'react'
import { useForm } from 'react-hook-form'
import TextAreaField from '../Form/TextAreaField'
import TextInputField from '../Form/TextInputField'
import TableBase from './TableBase'

type Inputs = {
  name: string
  description: string
}

const getServiceById = (id:number) => {
  return axios.get(`${import.meta.env.VITE_API_BASE_URL}Services/${id}`)
}

const getServices = (page:number) => {
  return axios.get(`${import.meta.env.VITE_API_BASE_URL}Services`, {
    params: {
      page,
      pageLength: 8,
      order: "id"
    }
  }).then(r => {
    return r.data
  })
}

const updateService = (id:number, data:any) => {
  console.log(id, data)
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

const ServicesTable = () => {
  const [formData, setFormData] = useState<any>(null)

  const {
    register,
    setValue,
    handleSubmit,
    formState: { errors },
  } = useForm<Inputs>()

  const columns = [
    { title: 'Name', minWidth: '100px', render: (data:any) => data.name },
    { title: 'Description', minWidth: '100px', render: (data:any) => data.description }
  ];

  const formContent = <>
    <TextInputField
      label="Name"
      name="name"
      defaultValue={formData?.name}
      placeholder="Service name"
      register={register}
      errors={errors}
      setValue={setValue}
      options={{required:true, minLength:2}}
    />
    <TextAreaField
      label="Description"
      name="description"
      defaultValue={formData?.description}
      placeholder="Service description"
      register={register}
      errors={errors}
      setValue={setValue}
      options={{minLength:4}}
    />
  </>

  return <TableBase 
    title="Services"
    columns={columns}
    formContent={formContent}
    formData={formData}
    nameProperty="name"
    languageResourceKey="services"
    setFormData={setFormData}
    handleSubmit={handleSubmit}
    get={getServices}
    getById={getServiceById}
    post={createService}
    update={updateService}
    delete={deleteService}
  />
};

export default ServicesTable
