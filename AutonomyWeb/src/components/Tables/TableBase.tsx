import DataTable, { Column, SearchResults } from './DataTable'
import { ReactNode, useEffect, useState } from 'react'
import { HiPencilSquare } from "react-icons/hi2"
import { GoTrash } from "react-icons/go"
import ActionButton from '../Buttons/ActionButton'
import ModalForm from '../Modals/ModalForm'
import { FaPlus } from "react-icons/fa"
import { SubmitHandler, UseFormHandleSubmit } from 'react-hook-form'
import toast, { Toaster } from 'react-hot-toast'
import ModalConfirm from '../Modals/ModalConfirm'
import { PiWarning } from "react-icons/pi";
import { formatMessage } from '../../language-utils'

interface TableBaseProps<Inputs> {
  title:string
  formContent:JSX.Element
  nameProperty:string
  languageResourceKey:string
  columns:Column[]
  formData:any
  handleSubmit:UseFormHandleSubmit<Inputs, undefined>
  setFormData:React.Dispatch<any>
  get:(page:number) => Promise<any>
  getById:(id:any) => Promise<any>
  post:(data:any) => Promise<any>
  update:(id:any, data:any) => Promise<any>
  delete:(id:any) => Promise<any>
}

const messageOnCreate = formatMessage("en", "actions", "onCreate", ["resource_type", "service"])
  const messageOnUpdate = formatMessage("en", "actions", "onUpdate", ["resource_type", "service"])
  const messageOnDelete = formatMessage("en", "actions", "onDelete", ["resource_type", "service"])
  const messageOnDuplicate = formatMessage("en", "errors", "onDuplicate", ["resource_type", "service"])
  const deleteConfirmation = formatMessage("en", "confirmation", "deleting", ["resource_type", "service"])

const TableBase = <Inputs,>(props:TableBaseProps<Inputs>) => {
  const [tableData, setTableData] = useState<SearchResults>(null)
  const [tablePage, setTablePage] = useState<number>(0)
  const [isTableLoading, setTableLoading] = useState<boolean>(false)
  const [isFormLoading, setFormLoading] = useState<boolean>(false)
  const [isFormVisible, setFormVisible] = useState<boolean>(false)
  const [isFormSubmitting, setFormSubmitting] = useState<boolean>(false)
  const [dataToDelete, setDataToDelete] = useState<any>(null)
  const [isDeleting, setIsDeleting] = useState<boolean>(false)
  const [deleteConfirmationMessage, setDeleteConfirmationMessage] = useState<ReactNode>(null)

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

  const handlePageChange = page => {
    props.get(page).then(r => {
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
    handlePageChange(0)
  }, [])

  const onSubmit: SubmitHandler<Inputs> = (data) => {
    setFormSubmitting(true)
    
    const result = props.formData == null ? props.post(data) : props.update(props.formData.id, data)

    result.then(r => {
      handlePageChange(tablePage)
      handleFormClose()
      toast.success(props.formData == null ? messageOnCreate : messageOnUpdate)
    }).catch(r => {
      setFormSubmitting(false)

      console.log(r)
      if (r.status == 409) {
        toast.error(messageOnDuplicate)
      } else {
        const errors = r.response.data.errors
        
        if (Array.isArray(errors)) {
          errors.forEach(error => {
            toast.error(error)
          })
        } else if (typeof errors === 'object' && errors !== null) {
          Object.entries(errors).forEach(error => {
            toast.error(error[1].toString())
          })
        }
      }
    })
  }
  
  const handleFormClose = () => {
    setFormVisible(false)
    props.setFormData(null)
    setFormSubmitting(false)
  }

  const handleDelete = (data:any) => {
    setIsDeleting(true)

    props.delete(data.id).then(r => {
      toast.success(messageOnDelete)
      handlePageChange(tablePage)
      setIsDeleting(false)
      setDataToDelete(null)
    })
  }
  
  const showForm = (data?) => {
    if (data) {
      setFormLoading(true)

      props.getById(data.id).then(r => {
        setFormLoading(false)
        props.setFormData(r.data)
      })
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

  const createButton = 
    <div style={{marginLeft: 'auto'}}>
      <span style={{float:'right'}}>
        <button onClick={() => showForm()} className="btn-success">
          <FaPlus />
        </button>
      </span>
    </div>

  return (
    <>
      <DataTable 
        title={props.title}
        top={createButton} 
        columns={columns} 
        data={tableData} 
        loading={isTableLoading}
        onPageChange={handlePageChange}
      />
      <ModalForm
        loading={isFormLoading} 
        visible={isFormVisible} 
        content={props.formContent} 
        onSubmit={props.handleSubmit(onSubmit)}
        submitting={isFormSubmitting}
        onClose={handleFormClose}
      />
      <ModalConfirm
        color="red"
        icon={PiWarning}
        title={deleteConfirmation.title}
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
