import React from 'react'
import * as BsIcons from 'react-icons/bs'
import * as AiIcons from 'react-icons/ai'
import * as FaIcons from 'react-icons/fa'

export const SidebarData = [
    {
        title: 'Home',
        path: '/',
        icon: <AiIcons.AiFillHome />,
        cName: 'nav-text'
    },
    {
        title: 'Promotion',
        path: '/promotion',
        icon: <FaIcons.FaCoins />,
        cName: 'nav-text'
    },
    {
        title: 'About',
        path: '/about',
        icon: <BsIcons.BsPeopleFill />,
        cName: 'nav-text'
    }
]