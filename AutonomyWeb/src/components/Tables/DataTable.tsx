import { ReactNode, useEffect, useState } from 'react';
import { ThreeDots } from 'react-loader-spinner'

interface Column {
    title:string,
    minWidth?:string,
    useFillColor?:boolean,
    render:(data:object) => any
}

interface DataTableProps {
    title:string,
    top?:ReactNode,
    columns:Column[],
    callback: (page:number) => Promise<any>
}

interface Results {
    selected:any[],
    page:number,
    pageLength:number,
    totalResults:number,
    totalPages:number
}

const DataTable = (props:DataTableProps) => {
    const [data, setData] = useState<Results>();
    const [pageLoading, setPageLoading] = useState<number>(null);
    const [pageSelected, setPageSelected] = useState<number>(0);
    const [pages, setPages] = useState<JSX.Element[]>([]);

    const handleFetch = (page:number) => {
        props.callback(page).then(data => {
            setData(data)
            setPageLoading(null)
        })

        setPageLoading(page)
    }

    useEffect(() => {
        handleFetch(0)
    }, [])

    useEffect(() => {
        if (!data) {
            return
        }
        
        const _pages:JSX.Element[] = []

        for (let i = 0; i < data.totalPages; i++) {
            _pages.push(
                <a key={i} onClick={() => handlePageChange(i)} className={`page ${pageSelected == i && "selected"}`} href="#">{i+1}</a>   
            );

            setPages(_pages)
        }
    }, [data, pageSelected])

    const handlePageChange = (page:number) => {
        if (page == pageSelected) {
            return
        }

        setPageSelected(page)
        handleFetch(page)
    }

    return (
        <div className="rounded-sm border border-stroke bg-white px-5 pt-6 pb-2.5 shadow-default dark:border-strokedark dark:bg-boxdark sm:px-7.5 xl:pb-1" style={{paddingBottom:9}}>
                <div style={{display:'flex', alignItems:'center', gap: 20, marginBottom: 23}}>
                    <span>
                        <h4 className="text-xl font-semibold text-black dark:text-white">
                            {props.title}
                        </h4> 
                    </span>
                    {pageLoading != null &&
                        <ThreeDots
                            color="#5361ca"
                            width="40px"
                            height="20px"
                            wrapperStyle={{display: "inline-block", height:"100%"}}
                        />
                    }
                    {props.top && props.top}           
                </div>
            

            <div className="datatable-container max-w-full overflow-x-auto" style={{position: 'relative'}}>
                <div style={{display: 'flex', flexDirection: 'column'}}>
                    <table className="w-full table-auto" style={{minHeight: '150px'}}>
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
                            {data && data.selected.map((row, rowIndex) => (
                                <tr key={rowIndex}>
                                    {props.columns.map((column, columnIndex) => (
                                        <td key={columnIndex} className="border-b border-[#eee] py-5 px-4 dark:border-strokedark">
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
                            {pages}
                        </div>
                    </div>
                </div>
                {pageLoading != null && (<div className="loading-overlay"></div>)}
            </div>
        </div>
    )
}

export default DataTable

