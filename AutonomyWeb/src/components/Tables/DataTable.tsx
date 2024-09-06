import { ReactNode, useEffect, useState } from 'react';
import { IconType } from 'react-icons';
import { ThreeDots } from 'react-loader-spinner'


export interface Column {
    title:ReactNode,
    minWidth?:string,
    useFillColor?:boolean,
    render:(data:object) => any
}

interface DataTableProps {
    icon?:IconType,
    displayHeader?:boolean
    title?:string,
    topRight?:ReactNode,
    topLeft?:ReactNode
    onPageChange?:(page:number) => void
    columns:Column[],
    loading?:boolean,
    data:SearchResults
}

export interface SearchResults {
    selected:any[],
    page?:number,
    totalPages?:number
}

const DataTable = (props:DataTableProps) => {
    const [pageSelected, setPageSelected] = useState<number>(0);
    const [pages, setPages] = useState<JSX.Element[]>([]);
    
    useEffect(() => {
        if (!props.data?.totalPages) {
            return
        }
        
        const _pages:JSX.Element[] = []

        for (let i = 0; i < props.data.totalPages; i++) {
            _pages.push(
                <a key={i} onClick={() => handlePageChange(i)} className={`page ${pageSelected == i && "selected"}`} href="#">{i+1}</a>   
            );

            setPages(_pages)
        }

        setPageSelected(props.data.page)
    }, [props.data, pageSelected])

    const handlePageChange = (page:number) => {
        if (page == pageSelected) {
            return
        }

        setPageSelected(page)
        props.onPageChange(page)
    }

    return (
        <div style={{position:'relative'}}>
            <div className="datatable-container" style={{paddingBottom:13}}>
                {props.displayHeader && <div style={{display:'flex', alignItems:'center', gap:25, marginBottom: 19}}>
                    <span>
                        <h4 className="text-xl font-semibold text-black dark:text-white">
                            <div style={{display:'flex', alignItems:'center', gap:10}}>
                                {props.icon && <props.icon />}
                                {props.title}
                            </div>
                        </h4> 
                    </span>
                    {props.topLeft && props.topLeft}   
                        {props.loading &&
                            <ThreeDots
                                color="#5361ca"
                                width={40}
                                height={20}
                                
                            />
                        }
                    <div style={{ marginLeft:'auto'}}>
                        {props.topRight && props.topRight}           
                    </div>
                </div>}

                <div className="datatable-body max-w-full overflow-x-auto" style={{position: 'relative'}}>
                    <div style={{display: 'flex', flexDirection: 'column'}}>
                        <table className="w-full table-auto" >
                            <thead>
                                <tr className="bg-gray-2 text-left uppercase dark:bg-meta-4">
                                    {props.columns.map((column, index) => (
                                        <th key={index} className={`min-w-[${column.minWidth}] py-4 px-4 font-medium text-black dark:text-white`}>
                                            {column.title}
                                        </th>
                                    ))}
                                </tr>
                            </thead>
                            <tbody >
                                {props.data && props.data.selected.map((row, rowIndex) => (
                                    <tr key={rowIndex}>
                                        {props.columns.map((column, columnIndex) => (
                                            <td key={columnIndex} className="border-b border-[#eee] px-4 dark:border-strokedark" style={{height:64}}>
                                                <p className={column.useFillColor ? "" : "text-black dark:text-white"}>
                                                    {column.render(row)}
                                                </p>
                                            </td>
                                        ))}
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                        <div style={{alignSelf: 'flex-end'}}>
                            <div style={{display:'flex', justifyContent: 'space-between', alignItems: 'center', gap: 4, paddingTop: 5, paddingBottom: 12 }}>
                                {props.data?.totalPages && props.data.selected.length >= 1 && pages}
                            </div>
                        </div>
                    </div>      
                </div>
            </div>
            {props.loading && (<div className="loading-overlay"></div>)}
        </div>
    )
}

export default DataTable

