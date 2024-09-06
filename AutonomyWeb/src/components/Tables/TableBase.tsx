import DataTable, { Column, SearchResults } from './DataTable'
import { ReactNode, useEffect, useState } from 'react'
import { HiPencilSquare } from "react-icons/hi2"
import { GoTrash } from "react-icons/go"
import ActionButton from '../Buttons/ActionButton'
import ModalForm from '../Modals/ModalForm'
import { FaPlus } from "react-icons/fa"
import { SubmitHandler, UseFormHandleSubmit, UseFormReset } from 'react-hook-form'
import toast, { Toaster } from 'react-hot-toast'
import ModalConfirm from '../Modals/ModalConfirm'
import { PiWarning } from "react-icons/pi";
import { formatMessage } from '../../language-utils'
import { handleErrors } from '../../ui-utils'

export type FormState = "creating" | "updating"

interface TableBaseProps<Inputs> {
  title:string
  formContent:JSX.Element
  idProperty:any
  nameProperty:string
  resourceName:string
  columns:Column[]
  formData:any
  formInactive?:boolean
  setFormData:React.Dispatch<any>
  handleFormSubmit:UseFormHandleSubmit<Inputs, undefined>
  resetForm:UseFormReset<Inputs>
  setFormState?:React.Dispatch<FormState>
  get:(page:number, searchTerm?:string) => Promise<any>
  getById:(id:any) => Promise<any>
  post:(data:any) => Promise<any>
  update:(id:any, data:any) => Promise<any>
  delete:(id:any) => Promise<any>
}

let searchTimeout = null

const TableBase = <Inputs,>(props:TableBaseProps<Inputs>) => {
  const [resourceName, setResourceName] = useState<string>(null)
  const [messageOnCreate, setMessageOnCreate] = useState<string>(null)
  const [messageOnUpdate, setMessageOnUpdate] = useState<string>(null)
  const [messageOnDelete, setMessageOnDelete] = useState<string>(null)
  const [messageOnDuplicate, setMessageOnDuplicate] = useState<string>(null)
  const [deleteConfirmation, setDeleteConfirmation] = useState<any>(null)
  const [searchTerm, setSearchTerm] = useState<string>(null)
  const [tableData, setTableData] = useState<SearchResults>(null)
  const [tablePage, setTablePage] = useState<number>(0)
  const [isTableLoading, setTableLoading] = useState<boolean>(false)
  const [isFormLoading, setFormLoading] = useState<boolean>(false)
  const [isFormVisible, setFormVisible] = useState<boolean>(false)
  const [isFormSubmitting, setFormSubmitting] = useState<boolean>(false)
  const [dataToDelete, setDataToDelete] = useState<any>(null)
  const [isDeleting, setDeleting] = useState<boolean>(false)
  const [deleteConfirmationMessage, setDeleteConfirmationMessage] = useState<ReactNode>(null)

  useEffect(() => {
    setMessageOnCreate(formatMessage("en", "actions", "onCreate", ["resource_type", resourceName]))
    setMessageOnUpdate(formatMessage("en", "actions", "onUpdate", ["resource_type", resourceName]))
    setMessageOnDelete(formatMessage("en", "actions", "onDelete", ["resource_type", resourceName]))
    setMessageOnDuplicate(formatMessage("en", "errors", "onDuplicate", ["resource_type", resourceName]))
    setDeleteConfirmation(formatMessage("en", "confirmation", "deleting", ["resource_type", resourceName]))
  }, [resourceName])

  useEffect(() => {
    if (dataToDelete) {
      const arr = deleteConfirmation.body.split("{resource_name}")

      if (arr.length > 1) {
        setDeleteConfirmationMessage([
          <span key={0}>{arr[0]}</span>,
          <b key={1}>{dataToDelete[props.nameProperty]}</b>, 
          <span key={2}>{arr[1]}</span>
        ])
      }
      
    }
  }, [dataToDelete])

  useEffect(() => {
    handlePageChange(tablePage)
  }, [searchTerm])

  const handlePageChange = page => {
    props.get(page, searchTerm).then(r => {
      if (page >= r.totalPages) {
        handlePageChange(r.totalPages-1)  
        return
      }

      setTableData(r)
      setTableLoading(false)
    });

    setTablePage(page)
    setTableLoading(true)
  }

  useEffect(() => {
    setResourceName(props.resourceName)
    handlePageChange(0)
  }, [])

  const onSubmit: SubmitHandler<Inputs> = (data) => {
    setFormSubmitting(true)
    
    const result = props.formData ? props.update(props.formData[props.idProperty], data) : props.post(data)

    result.then(r => {
      handlePageChange(tablePage)
      toast.success(props.formData ? messageOnUpdate : messageOnCreate)

      if (props.formData) {
        handleFormClose()
      } else {
        if (r && r[props.idProperty]) {
          const newData = {...data}
          newData[props.idProperty] = r[props.idProperty]
          setFormSubmitting(false)
          props.setFormData(newData)
        } else {
          handleFormClose()
        }
      }
    }).catch(r => {
      setFormSubmitting(false)
      handleErrors(r, messageOnDuplicate)
    })
  }
  
  const handleFormClose = () => {
    setFormVisible(false)
    props.setFormData(null)
    setFormSubmitting(false)
    props.resetForm()
    props.setFormState && props.setFormState(null)
  }

  const handleDelete = (data:any) => {
    setDeleting(true)

    props.delete(data[props.idProperty]).then(r => {
      toast.success(messageOnDelete)
      handlePageChange(tablePage)
      setDeleting(false)
      setDataToDelete(null)
    })
  }
  
  const handleSearch = e => {
    if (searchTimeout) {
      clearTimeout(searchTimeout)
    }

    setTableLoading(true)

    searchTimeout = setTimeout(() => {
      console.log(e.target.value)
      setSearchTerm(e.target.value)
    }, 700)
  }
  
  const showForm = (data?) => {
    if (data) {
      props.setFormState && props.setFormState("updating")
      setFormLoading(true)

      props.getById(data[props.idProperty]).then(r => {
        setFormLoading(false)
        props.setFormData(r)
      })
    } else {
      props.setFormState && props.setFormState("creating")
    }

    setFormVisible(true)
  }

  const columns = [...props.columns,
    { title: 'Actions', useFillColor: true, render: (data:any) => 
      <span className='actions'>
        <ActionButton icon={HiPencilSquare} onClick={() => showForm(data)}/>
        <ActionButton icon={GoTrash} onClick={() => setDataToDelete(data)}/>
      </span>
    }
  ];

  const search = 
    <div style={{width:300}}>
      <input placeholder="Type to search..." onInput={handleSearch} />
    </div>

  const createButton = 
    <button onClick={() => showForm()} className="btn-success">
      <FaPlus />
    </button>

  return (
    <>
      <DataTable 
        title={props.title}
        topLeft={search}
        topRight={createButton} 
        columns={columns} 
        data={tableData} 
        displayHeader={true}
        loading={isTableLoading}
        onPageChange={handlePageChange}
      />
      <ModalForm
        loading={isFormLoading} 
        inactive={props.formInactive}
        visible={isFormVisible} 
        content={props.formContent} 
        onSubmit={props.handleFormSubmit(onSubmit)}
        submitting={isFormSubmitting}
        onClose={handleFormClose}
        saveButtonLabel={props.formData ? "Save" : "Create"}
      />
      <ModalConfirm
        color="red"
        icon={PiWarning}
        title={deleteConfirmation?.title}
        body={deleteConfirmationMessage}
        visible={dataToDelete != null}
        onClose={() => setDataToDelete(null)}
        onConfirm={() => handleDelete(dataToDelete)}
        confirming={isDeleting}
      />
      <Toaster
        position="bottom-right"
      />
    </>
  );
};

export default TableBase
