import { useState } from 'react'
import {
    Menu, Image, Dropdown, Icon
} from "semantic-ui-react";

export default function MainMenu() {
    
    // eslint-disable-next-line no-unused-vars
    const [activeMenuItem, setActiveMenuItem] = useState("dashboard");
    return (
            <>
            <Menu
                fixed='left'
                vertical
                inverted
                className='left-menu'
                style={{ background: '#7d38b3' }}>
            <Menu.Item header={true}>
                <Image
                    centered
                    src='https://react.semantic-ui.com/images/wireframe/square-image.png' avatar />
                &nbsp;&nbsp;Makoto Edamura
            </Menu.Item>
            <Menu.Item
                name='dashboard'
                active={activeMenuItem === 'dashboard'}>
                Dashboard
            </Menu.Item>
            <Menu.Item>
                Categories
            </Menu.Item>
            <Menu.Item
                name='incomes'
                active={activeMenuItem === 'incomes'}>
                Incomes
            </Menu.Item>
            <Menu.Item
                name='expenses'
                active={activeMenuItem === 'expenses'}>
                Expenses
            </Menu.Item>
            </Menu>
            <Menu
                fixed='top'
                inverted
                className='top-menu'
                style={{ background: '#7d38b3' }}>
                    
                <Dropdown item icon={<Icon name='bars' />}>
                    <Dropdown.Menu style={{ width: '200px !important' }} fluid >
        <Dropdown.Item >  <Image
                    centered
                    src='https://react.semantic-ui.com/images/wireframe/square-image.png' avatar />
                &nbsp;Makoto Edamura</Dropdown.Item>
        <Dropdown.Divider />
        <Dropdown.Header>Dropdown</Dropdown.Header>
        <Dropdown.Item>Option 1</Dropdown.Item>
        <Dropdown.Item>Option 2</Dropdown.Item>
        <Dropdown.Item>Option 3</Dropdown.Item>
      </Dropdown.Menu>
    </Dropdown>
 
            </Menu>
            </>
)
}