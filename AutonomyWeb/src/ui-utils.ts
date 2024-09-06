import toast from "react-hot-toast"

export function handleErrors(response:any, messageOnDuplicate:string) {
    if (response.status == 409) {
        toast.error(messageOnDuplicate)
      } else {
        const errors = response.response.data.errors
        
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
}