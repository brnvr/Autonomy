import axios from 'axios'
import DataTable from './DataTable'
import { useState } from 'react'
import { HiPencilSquare } from "react-icons/hi2"
import { GoTrash } from "react-icons/go"
import ActionButton from '../Buttons/ActionButton'
import ModalForm from '../Modals/ModalForm'
import { FaPlus } from "react-icons/fa"
import { SubmitHandler, useForm } from 'react-hook-form'
import { handleErrors } from '../../react-hook-form-utils'
import TextArea from '../Form/TextArea'
import Input from '../Form/Input'


const getServiceById = (id:number) => {
  return axios.get(`${import.meta.env.VITE_API_BASE_URL}Services/${id}`)
}

const getServices = (page:number) => {
  return axios.get(`${import.meta.env.VITE_API_BASE_URL}Services`, {
    params: {
      page,
      pageLength: 2
    }
  }).then(r => {
    return r.data
  })
}

const updateService = (id:number, data:any) => {
  return axios.put(`${import.meta.env.VITE_API_BASE_URL}Services/${id}`, data).then(r => {
    console.log(r)
    return r.data
  })
}

type Inputs = {
  example: string
  exampleRequired: string
}

const ServicesTable = () => {
  const [currentService, setCurrentService] = useState<any>(null)
  const [isFormLoading, setFormLoading] = useState<boolean>(false)
  const [isFormVisible, setFormVisible] = useState<boolean>(false)

  const {
    register,
    handleSubmit,
    watch,
    formState: { errors },
  } = useForm<Inputs>()
  const onSubmit: SubmitHandler<Inputs> = (data) => console.log(data)

  const formContent = <>
    <Input
      defaultValue={currentService?.name}
      placeholder="Service name"
      register={register}
      errors={errors}
      fieldName="example"
      options={{minLength:2}}
    />
    <TextArea 
      placeholder="Service description"
      register={register}
      errors={errors}
      fieldName="exampleRequired"
      options={{minLength:4}}
    />
  </>

  const onSubmitServiceForm = e => {
    //updateService(currentService.id, formData)
    handleSubmit(onSubmit)(e)
    console.log(errors)
  }
  
  const onCloseServiceForm = () => {
    setFormVisible(false)
    setCurrentService(null)
  }

  
  const showServiceForm = (e, data?) => {
      if (data) {
        setFormLoading(true)

        getServiceById(data.id).then(r => {
          setFormLoading(false)
          setCurrentService(r.data)
        })
      }

    setFormVisible(true)
  }

  const columns = [
    { title: 'Name', minWidth: '100px', render: (data:any) => data.name },
    { title: 'Description', minWidth: '100px', render: (data:any) => data.description },
    { title: 'Actions', useFillColor: true, render: (data:any) => 
      <span className='actions'>
        <ActionButton icon={<HiPencilSquare />} onClick={e => showServiceForm(e, data)}/>
        <ActionButton icon = {<GoTrash />} onClick={() => {}}/>
      </span>
    }
  ];

  const newService = 
    <div style={{marginLeft: 'auto'}}>
      <span style={{float:'right'}}>
        <button
          onClick={showServiceForm}
          className="inline-flex rounded-md bg-meta-3 py-3 px-4 text-white hover:bg-opacity-90"
        >
          <FaPlus />
        </button>
      </span>
    </div>

  return (
    <>
      <ModalForm loading={isFormLoading} visible={isFormVisible} content={formContent} onSubmit={onSubmitServiceForm} onClose={onCloseServiceForm}/>
      <DataTable title="Services" top={newService} columns={columns} callback={getServices} />
    </>
  );
};

export default ServicesTable
