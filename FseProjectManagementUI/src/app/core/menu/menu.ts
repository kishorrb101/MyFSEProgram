export interface Menu {
  text: string;
  heading?: boolean;
  link?: string;     // internal route links
  elink?: string;    // used only for external links
  target?: string;   // anchor target="_blank|_self|_parent|_top|framename"
  icon?: string;
  alert?: string;
  submenu?: Array<Menu>;
}


const Users = {
  text: 'Users',
  link: '/user/list',
  icon: 'fas fa-users',
};

const Projects = {
  text: 'Projects',
  link: '/project/list',
  icon: 'fas fa-umbrella'
};

const Tasks = {
  text: 'Tasks',
  link: '/task/list',
  icon: 'fas fa-tasks'
};

const AboutUs = {
  text: 'About Us',
  link: '/about-us/about-us',
  icon: 'fa fa-info'
};

export const menus = {
  users: Users,
  tasks: Tasks,
  projects: Projects,
  aboutus: AboutUs,
};
