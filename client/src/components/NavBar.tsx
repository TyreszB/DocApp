import React from 'react';

const NavBar = () => {
  return (
    <nav className="navbar bg-white shadow-lg border-b border-gray-200">
        <div className="w-full px-4 py-3">
            <div className="flex justify-between items-center">
                <div className="flex space-x-7">
                    <div className="flex items-center">
                        <span className="text-xl font-bold text-gray-800">DocApp</span>
                    </div>
                </div>
            </div>
        </div>
    </nav>
  )
}

export default NavBar