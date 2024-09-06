import { useEffect, useState } from "react"
import ActionButton from "../Buttons/ActionButton"
import SearchableSelectField from "../Form/SearchableSelectField"
import SwitchableInput from "../Form/SwitchableInput"
import DataTable from "../Tables/DataTable"
import ModalForm from "./ModalForm"
import axios from "axios"
import { TfiArrowCircleDown, TfiArrowCircleUp } from "react-icons/tfi"
import { GoTrash } from "react-icons/go"
import { FaPlus } from "react-icons/fa"

interface BudgetsModalProps {
    visible?:boolean
    onClose:()=>void
    onSubmit:(data:any)=>void
}

const getCurrencies = (searchTerm?:string) => {
    return axios.get(`${import.meta.env.VITE_API_BASE_URL}Currencies`, {
    params: {
        searchTerm
    }
    }).then(r => r.data)
}

const BudgetsModal = (props:BudgetsModalProps) => {
    const [currencyOptions, setCurrencyOptions] = useState<any>(null)
    const [currency, setCurrency] = useState<any>(null)
    const [budgetData, setBudgetData] = useState<any[]>([])

    const updateCurrencies = (searchTerm?:string) => {
        getCurrencies(searchTerm).then(r => setCurrencyOptions(r.map(option => { 
          return {
            value: option.id,
            text:`${option.code} (${option.name})`,
            data: option
          }
        })))
      }

      const addBudgetItem = () => {
        const newBudgetData = [...budgetData, {
          name:"batatinha", 
          quantity:5, 
          unitPrice:"50", 
          duration:"5", 
          durationTimeUnit:3, 
          durationTimeUnitDescription: {
            singular: "Day",
            plural: "Days"
          }} 
        ]
    
        setBudgetData(newBudgetData)
      }
      
      const removeBudgetItem = (data:any) => {
        const _budgetData = [...budgetData]
        const index = _budgetData.findIndex(value => value == data)
    
        _budgetData.splice(index, 1);
        setBudgetData(_budgetData)
      }
    
      const updateBudgetItem = (data:any, fieldName:string, newValue:any):void => {
        const _budgetData = [...budgetData]
        const index = _budgetData.findIndex(value => value == data)
    
        _budgetData[index][fieldName] = newValue
    
        setBudgetData(_budgetData)
      }

      const selectTimeUnit = (data:any) => (
        <select className="select-time-unit" style={{width:"160px"}} value={data.durationTimeUnit} onChange={(e) => handleChangeBudgetItemTimeUnit(e, data)}>
          <option value={null}></option>
          <option value={0}>Minute</option>
          <option value={1}>Hour</option>
          <option value={2}>Half day</option>
          <option value={3}>Day</option>
          <option value={4}>Week</option>
          <option value={5}>Month</option>
        </select>)

      const handleChangeBudgetItemTimeUnit = (e:any, data:any) => {
        const value = parseInt(e.target.value)
        const description:any = {}
    
        switch (value) {
          case null || undefined || NaN:
            break

          case 0:
            description.singular = "Minute"
            description.plural = "Minutes"
            break
          
          case 1:
            description.singular = "Hour"
            description.plural = "Hours"
            break
    
          case 2:
            description.singular = "Half day"
            description.plural = "Half days"
            break
    
          case 3:
            description.singular = "Day"
            description.plural = "Days"
            break
    
          case 4:
            description.singular = "Week"
            description.plural = "Weeks"
            break
    
          case 5:
            description.singular = "Month"
            description.plural = "Months"
            break
    
          default:
            console.warn("Unknown time unit index: " + value)
            break
        }
    
        updateBudgetItem(data, "durationTimeUnit", value)
        updateBudgetItem(data, "durationTimeUnitDescription", description)
      }

      const moveUpBudgetItem = (data:any) => {
        const _budgetData = [...budgetData]
        const index = _budgetData.findIndex(value => value == data)
    
        if (index == 0) return
    
        const element = _budgetData.splice(index, 1)[0];
    
        _budgetData.splice(index-1, 0, element);
        setBudgetData(_budgetData)
      }
    
      const moveDownBudgetItem = (data:any) => {
        const _budgetData = [...budgetData]
        const index = _budgetData.findIndex(value => value == data)
    
        if (index >= budgetData.length-1) return
    
        const element = _budgetData.splice(index, 1)[0];
    
        _budgetData.splice(index+1, 0, element);
        setBudgetData(_budgetData)
      }
    
      useEffect(() => {
        if (props.visible) {
          updateCurrencies()
        }
      }, [props.visible])

    return props.visible && <ModalForm 
        onSubmit={() => props.onSubmit({
            currencyId: currency?.id ?? 0,
            items: budgetData
        })}
        onClose={() => props.onClose()}
        width={1260}
        content={<>
          <SearchableSelectField 
            label = "Currency"
            onInput={e => updateCurrencies(e.target.value)}
            onLeave={updateCurrencies}
            onSelect={(option) => setCurrency(option.data)}
            options={currencyOptions || []}
          />
          
          <DataTable
            columns={[
              { title: 'Name', render: (data:any) => 
                <SwitchableInput 
                  placeholder="Name"
                  defaultValue={data.name}
                  width='160px'
                  onSave={val => updateBudgetItem(data, "name", val)}
                /> },
              { title: 'Quantity', render: (data:any) =>
                <SwitchableInput
                  placeholder="Quantity"
                  defaultValue={data.quantity}
                  type="number"
                  width='160px'
                  onSave={val => updateBudgetItem(data, "quantity", val)}/> },
              { title: 'Rate', render: (data:any) =>
                <SwitchableInput
                  placeholder="Rate"
                  defaultValue={data.unitPrice}
                  format={val => `${currency ? (currency.symbol + " ") : ""}${val}${data.durationTimeUnitDescription?.singular ? (`/${data.durationTimeUnitDescription.singular}`) : ""}`}
                  width='160px'
                  onSave={val => updateBudgetItem(data, "unitPrice", val)}
                /> },
              { title: 'Duration', render: (data:any) =>
                <SwitchableInput
                  placeholder="Duration"
                  defaultValue={data.duration}
                  type="number"
                  format={val => `${data.durationTimeUnitDescription?.plural ? (`${val} ${data.durationTimeUnitDescription.plural}`) : ""}`}
                  width='160px'
                  onSave={val => updateBudgetItem(data, "duration", val)}
                /> },
              { title: 'Time unit', render: (data:any) => selectTimeUnit(data)},
              { title: 'Actions', useFillColor: true, render: (data:any) => 
                <span className='actions'>
                  <ActionButton
                    icon={TfiArrowCircleUp}
                    onClick={() => moveUpBudgetItem(data)} 
                  />
                  <ActionButton
                    icon={TfiArrowCircleDown}
                    onClick={() => moveDownBudgetItem(data)}
                  />
                  <ActionButton
                    icon={GoTrash}
                    onClick={() => removeBudgetItem(data)}
                  />
                </span>
              }
            ]} 
            data={{
              selected:budgetData,
            }}        
          />
          <div style={{marginBottom:30}}>
            <button type="button" onClick={addBudgetItem} className="btn-success">
              <FaPlus />
            </button>
          </div>
        </>}
        visible={true}
      />
}

export default BudgetsModal